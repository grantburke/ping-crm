<script>
	import { inertia, page } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import OrganizationForm from '../../Shared/OrganizationForm.svelte'

	$: ({ contacts, organization } = $page.props)
</script>

<PageLayout title="Edit Organization">
	<OrganizationForm method="edit" model={organization} />

	<h2 class="mb-4 mt-8 text-2xl font-bold">Contacts</h2>
	<table
		class="mx-auto w-full table-auto rounded-lg bg-white px-8 py-4 shadow">
		<thead class="text-left">
			<th class="p-4">Name</th>
			<th class="p-4">City</th>
			<th class="p-4">Phone</th>
		</thead>
		<tbody>
			{#each contacts as contact}
				<tr class="text-left focus-within:bg-gray-100 hover:bg-gray-100">
					<td class="border-t p-4"
						>{contact.firstName} {contact.lastName}</td>
					<td class="border-t p-4">{contact.city}</td>
					<td class="border-t p-4">{contact.phone}</td>
					<td class="border-t">
						<a
							use:inertia
							class="grid place-items-center p-4 hover:underline"
							href={`/contacts/${contact.id}/edit`}>Edit</a>
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
</PageLayout>
