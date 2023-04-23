<script>
	import { inertia, page, router } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import debounce from '../../utils'

	let search = ''

	$: ({
		data: organizations,
		page: currentPage,
		perPage,
		total,
		pageNumbers,
		previousButtonDisabled,
		nextButtonDisabled,
	} = $page.props)

	function handle_search() {
		router.get('/organizations', { search })
	}
</script>

<PageLayout title="Organizations">
	<div class="flex items-center justify-between">
		<div class="flex w-1/2 items-center gap-4">
			<input
				type="search"
				class="w-1/2 rounded border-gray-200 shadow placeholder:text-gray-400"
				placeholder="Search..."
				bind:value={search}
				use:debounce={{
					search_value: search,
					func: handle_search,
					duration: 500,
				}} />
			<a href="/organizations" class="text-sm text-gray-500">Reset</a>
		</div>
		<a
			href="/organizations/create"
			class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600"
			>Create Organization</a>
	</div>
	<table class="mt-4 w-full table-auto rounded-lg bg-white shadow">
		<thead>
			<tr class="text-left">
				<th class="p-4">Name</th>
				<th class="p-4">City</th>
				<th class="p-4">Phone</th>
				<th class="p-4" />
			</tr>
		</thead>
		<tbody>
			{#each organizations as org}
				<tr class="text-left focus-within:bg-gray-100 hover:bg-gray-100">
					<td class="border-t p-4">{org.name}</td>
					<td class="border-t p-4">{org.city}</td>
					<td class="border-t p-4">{org.phone}</td>
					<td class="border-t">
						<a
							use:inertia
							class="grid place-items-center p-4 hover:underline"
							href="/organizations/{org.id}/edit">Edit</a>
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
	<div class="flex items-center py-4">
		<a
			use:inertia
			href={`/organizations?page=${currentPage - 1}`}
			class={`mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600 ${
				previousButtonDisabled ? 'pointer-events-none text-gray-400' : ''
			}`}>Prev</a>
		{#each pageNumbers as pageNumber}
			<a
				use:inertia
				href={`/organizations?page=${pageNumber}`}
				class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
				class:bg-white={currentPage == pageNumber}>
				{pageNumber}
			</a>
		{/each}
		<a
			use:inertia
			href={`/organizations?page=${currentPage + 1}`}
			class={`mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600 ${
				nextButtonDisabled ? 'pointer-events-none text-gray-400' : ''
			}`}>Next</a>
	</div>
</PageLayout>
