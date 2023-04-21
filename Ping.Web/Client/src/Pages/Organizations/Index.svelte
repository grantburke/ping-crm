<script>
	import { inertia, page } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'

	$: organizations = $page?.props?.organizations ?? []
</script>

<PageLayout title="Organizations">
	<div class="flex items-center justify-between">
		<div class="flex w-1/2 items-center gap-4">
			<input
				type="search"
				class="w-1/2 rounded border-gray-200 shadow placeholder:text-gray-400"
				placeholder="Search..." />
			<button class="text-sm text-gray-500">Reset</button>
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
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>Prev</button>
		<button
			class="mb-1 mr-1 rounded border bg-white px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>1</button>
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>2</button>
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>3</button>
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>4</button>
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>5</button>
		<button
			class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
			>Next</button>
	</div>
</PageLayout>
