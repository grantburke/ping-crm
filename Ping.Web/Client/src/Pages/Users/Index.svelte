<script>
	import { inertia, page } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'

	$: ({ users } = $page.props)
</script>

<PageLayout title="Users">
	<div class="flex items-center justify-end">
		<a
			href={`/users/create`}
			class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600"
			>Create User</a>
	</div>
	<table class="mt-4 w-full table-auto rounded-lg bg-white shadow">
		<thead>
			<tr class="text-left">
				<th class="p-4">Email</th>
				<th class="p-4">Role</th>
				<th class="p-4" />
			</tr>
		</thead>
		<tbody>
			{#each users as user}
				<tr>
					<td class="border-t p-4">{user.email}</td>
					<td class="border-t p-4">{user.role}</td>
					<td class="border-t">
						<a
							use:inertia
							class="grid place-items-center p-4 hover:underline"
							href={`/users/${user.id}/edit`}>Edit</a>
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
</PageLayout>
