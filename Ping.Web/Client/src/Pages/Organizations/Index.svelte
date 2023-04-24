<script>
	import { inertia, page, router } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import PaginatedTable from '../../Shared/PaginatedTable.svelte'

	const route = 'organizations'
	const columns = [
		{ display: 'Name', name: 'name' },
		{ display: 'City', name: 'city' },
		{ display: 'Phone', name: 'phone' },
	]

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
	<PaginatedTable
		{route}
		{columns}
		{currentPage}
		{perPage}
		{total}
		{pageNumbers}
		{previousButtonDisabled}
		{nextButtonDisabled}>
		<span slot="cta">
			<a
				href={`/${route}/create`}
				class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600"
				>Create Organization</a>
		</span>
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
	</PaginatedTable>
</PageLayout>
