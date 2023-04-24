<script>
	import { router } from '@inertiajs/svelte'
	import Pagination from './Pagination.svelte'
	import SearchFilter from './SearchFilter.svelte'

	export let route = ''
	export let columns = []
	export let currentPage = 1
	export let perPage = 0
	export let total = 0
	export let pageNumbers = []
	export let previousButtonDisabled = true
	export let nextButtonDisabled = true

	let search = ''
	function handle_search_filter_change(e) {
		search = e.detail.search
		currentPage = 1
		get_data()
	}

	let sort_column = ''
	let sort_direction = ''
	function handle_sort_change(column_name) {
		if (column_name == sort_column)
			sort_direction =
				sort_direction == 'ascending' ? 'descending' : 'ascending'
		else sort_direction = 'ascending'
		sort_column = column_name
		get_data()
	}

	function handle_page_change(e) {
		const { new_page } = e.detail
		currentPage = new_page
		get_data()
	}

	function get_data() {
		router.get(`/${route}`, {
			page: currentPage,
			search,
			sortColumn: sort_column,
			sortDirection: sort_direction,
		})
	}
</script>

<div class="flex items-center justify-between">
	<SearchFilter
		{route}
		on:search_filter_change={handle_search_filter_change} />
	<slot name="cta" />
</div>
<table class="mt-4 w-full table-auto rounded-lg bg-white shadow">
	<thead>
		<tr class="text-left">
			{#each columns as column}
				<th
					class="cursor-pointer p-4"
					on:click={() => handle_sort_change(column.name)}>
					{column.display}
					<span>
						{#if sort_column == column.name && sort_direction == 'descending'}
							&uarr;
						{:else if sort_column == column.name && sort_direction == 'ascending'}
							&darr;
						{/if}
					</span>
				</th>
			{/each}
			<th class="p-4" />
		</tr>
	</thead>
	<tbody>
		<slot />
	</tbody>
</table>
<Pagination
	{route}
	{currentPage}
	{perPage}
	{total}
	{pageNumbers}
	{previousButtonDisabled}
	{nextButtonDisabled}
	on:page_change={handle_page_change} />
