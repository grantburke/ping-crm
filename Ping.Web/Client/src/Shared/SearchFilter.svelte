<script>
	import debounce from '../utils'
	import { createEventDispatcher } from 'svelte'

	export let route = ''

	const dispatcher = createEventDispatcher()
	let search = ''

	function handle_search() {
		dispatcher('search_filter_change', { search })
	}
</script>

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
	<a href={`/${route}`} class="text-sm text-gray-500">Reset</a>
</div>
