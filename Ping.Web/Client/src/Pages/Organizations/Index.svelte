<script>
	import { inertia, page, router } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import Pagination from '../../Shared/Pagination.svelte'
	import SearchFilter from '../../Shared/SearchFilter.svelte'

	const route = 'organizations'

	$: ({
		data: organizations,
		page: currentPage,
		perPage,
		total,
		pageNumbers,
		previousButtonDisabled,
		nextButtonDisabled,
	} = $page.props)
</script>

<PageLayout title="Organizations">
	<div class="flex items-center justify-between">
		<SearchFilter {route} />
		<a
			href={`/${route}/create`}
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
							href={`/${route}/${org.id}/edit`}>Edit</a>
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
	<Pagination
		{route}
		{currentPage}
		{perPage}
		{total}
		{pageNumbers}
		{previousButtonDisabled}
		{nextButtonDisabled} />
</PageLayout>
