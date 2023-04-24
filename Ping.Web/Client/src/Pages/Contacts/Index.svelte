<script>
	import { inertia, page, router } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import PaginatedTable from '../../Shared/PaginatedTable.svelte'

	const route = 'contacts'
	const columns = [
		{ display: 'Name', name: 'firstname,lastname' },
		{ display: 'Organization', name: 'organization.name' },
		{ display: 'City', name: 'city' },
		{ display: 'Phone', name: 'phone' },
	]

	$: ({
		data: contacts,
		page: currentPage,
		perPage,
		total,
		pageNumbers,
		previousButtonDisabled,
		nextButtonDisabled,
	} = $page.props)
</script>

<PageLayout title="Contacts">
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
				>Create Contact</a>
		</span>
		{#each contacts as contact}
			<tr class="text-left focus-within:bg-gray-100 hover:bg-gray-100">
				<td class="border-t p-4">{contact.firstName} {contact.lastName}</td>
				<td class="border-t p-4">{contact.organization.name}</td>
				<td class="border-t p-4">{contact.city}</td>
				<td class="border-t p-4">{contact.phone}</td>
				<td class="border-t">
					<a
						use:inertia
						class="grid place-items-center p-4 hover:underline"
						href={`/${route}/${contact.id}/edit`}>Edit</a>
				</td>
			</tr>
		{/each}
	</PaginatedTable>
</PageLayout>
