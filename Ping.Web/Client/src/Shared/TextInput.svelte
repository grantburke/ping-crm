<script>
	export let id
	export let value
	export let label
	export let type = 'text'
	export let error

	let input

	export const focus = () => input.focus()
	export const select = () => input.select()

	$: props = {
		...$$restProps,
		class: 'mt-2 block w-full rounded-md border-0 px-3 py-2 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6',
	}

	function update(event) {
		value = event.target.value
	}
</script>

<div class={$$restProps.class}>
	<label for={id} class="block font-bold leading-6 text-gray-900"
		>{label}</label>
	<input
		{...props}
		bind:this={input}
		class:error
		{id}
		{type}
		{value}
		on:input={update} />

	{#if error}
		<div class="text-red-500">{error[0]}</div>
	{/if}
</div>
