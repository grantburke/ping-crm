<script>
	import { inertia, page, router } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import Pagination from '../../Shared/Pagination.svelte'
	import SearchFilter from '../../Shared/SearchFilter.svelte'

	const route = 'contacts'

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
	<div class="flex items-center justify-between">
		<SearchFilter {route} />
		<a
			href={`/${route}/create`}
			class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600"
			>Create Contact</a>
	</div>
	<table class="mt-4 w-full table-auto rounded-lg bg-white shadow">
		<thead>
			<tr class="text-left">
				<th class="p-4">Name</th>
				<th class="p-4">Organization</th>
				<th class="p-4">City</th>
				<th class="p-4">Phone</th>
				<th class="p-4" />
			</tr>
		</thead>
		<tbody>
			{#each contacts as contact}
				<tr class="text-left focus-within:bg-gray-100 hover:bg-gray-100">
					<td class="border-t p-4"
						>{contact.firstName} {contact.lastName}</td>
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
