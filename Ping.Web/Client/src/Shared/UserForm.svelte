<script>
	import { useForm } from '@inertiajs/svelte'
	import TextInput from './TextInput.svelte'

	export let method = 'create'
	export let model = {
		id: 0,
		email: '',
		password: '',
		role: 2,
	}
	export let roles = []

	let form = useForm(model)

	function handle_submit() {
		$form.clearErrors()
		if (method !== 'create') $form.put(`/users/${$form.id}/edit`)
		else $form.post(`/users/create`)
	}
</script>

<form
	class="mx-auto w-full rounded bg-white px-8 py-4 shadow"
	on:submit|preventDefault={handle_submit}>
	<TextInput
		id="email"
		class="py-2"
		type="email"
		label="Email"
		placeholder="Email"
		bind:value={$form.email}
		bind:error={$form.errors.email} />
	<TextInput
		id="password"
		class="py-2"
		type="password"
		label="Password"
		placeholder="Password"
		bind:value={$form.password}
		bind:error={$form.errors.password} />
	<div class="py-2">
		<label for="role" class="block font-bold leading-6 text-gray-900"
			>Role</label>
		<select
			id="role"
			class="mt-2 block w-full rounded-md border-0 px-3 py-2 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
			placeholder="Select an role"
			bind:value={$form.role}>
			<option value="" disabled>Select a role</option>
			{#each roles as role}
				<option value={role.id}>{role.name}</option>
			{/each}
		</select>
	</div>
	<div class="py-2">
		<button
			type="submit"
			class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600">
			Submit
		</button>
	</div>
</form>
