<script>
	import { router } from '@inertiajs/svelte'
	import debounce from '../utils'

	export let route = ''

	let search = ''

	function handle_search() {
		router.get(`/${route}`, { search })
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
