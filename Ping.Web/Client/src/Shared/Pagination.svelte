<script>
	import { inertia } from '@inertiajs/svelte'

	export let route = ''
	export let currentPage = 1
	export let perPage = 0
	export let total = 0
	export let pageNumbers = []
	export let previousButtonDisabled = true
	export let nextButtonDisabled = true
</script>

<div class="flex items-center justify-between">
	<div class="flex items-center py-4">
		<a
			use:inertia
			href={`/${route}?page=${currentPage - 1}`}
			class={`mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600 ${
				previousButtonDisabled ? 'pointer-events-none text-gray-400' : ''
			}`}>Prev</a>
		{#each pageNumbers as pageNumber}
			<a
				use:inertia
				href={`/${route}?page=${pageNumber}`}
				class="mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600"
				class:bg-white={currentPage == pageNumber}>
				{pageNumber}
			</a>
		{/each}
		<a
			use:inertia
			href={`/${route}?page=${currentPage + 1}`}
			class={`mb-1 mr-1 rounded border px-4 py-3 text-sm leading-4 hover:bg-white focus:border-indigo-600 focus:text-indigo-600 ${
				nextButtonDisabled ? 'pointer-events-none text-gray-400' : ''
			}`}>Next</a>
	</div>
	<div>
		<p class="text-sm text-gray-700">
			Showing
			<span class="font-medium">{(currentPage - 1) * perPage + 1}</span>
			to
			<span class="font-medium"
				>{nextButtonDisabled ? total : currentPage * perPage}</span>
			of
			<span class="font-medium">{total}</span>
			results
		</p>
	</div>
</div>
