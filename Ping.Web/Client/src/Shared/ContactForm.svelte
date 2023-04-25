<script>
	import { useForm } from '@inertiajs/svelte'
	import TextInput from './TextInput.svelte'

	export let method = 'create'
	export let model = {
		id: 0,
		firstName: '',
		lastName: '',
		email: '',
		phone: '',
		address: '',
		city: '',
		state: '',
		zipCode: '',
		organizationId: 0,
	}
	export let organizations = []

	let form = useForm(model)

	function handle_submit() {
		$form.clearErrors()
		if (method !== 'create') $form.put(`/contacts/${$form.id}/edit`)
		else $form.post(`/contacts/create`)
	}
</script>

<form
	class="mx-auto w-full rounded bg-white px-8 py-4 shadow"
	on:submit|preventDefault={handle_submit}>
	<TextInput
		class="py-2"
		id="first_name"
		label="First Name"
		placeholder="First Name"
		bind:value={$form.firstName}
		bind:error={$form.errors.firstName} />
	<TextInput
		class="py-2"
		id="last_name"
		label="Last Name"
		placeholder="Last Name"
		bind:value={$form.lastName}
		bind:error={$form.errors.lastName} />
	<TextInput
		id="email"
		class="py-2"
		type="email"
		label="Email"
		placeholder="Email"
		bind:value={$form.email}
		bind:error={$form.errors.email} />
	<TextInput
		id="phone"
		class="py-2"
		label="Phone"
		placeholder="Phone"
		bind:value={$form.phone}
		bind:error={$form.errors.phone} />
	<TextInput
		id="address"
		class="py-2"
		label="Address"
		placeholder="Address"
		bind:value={$form.address}
		bind:error={$form.errors.address} />
	<TextInput
		id="city"
		class="py-2"
		label="City"
		placeholder="City"
		bind:value={$form.city}
		bind:error={$form.errors.city} />
	<TextInput
		id="state"
		class="py-2"
		label="State"
		placeholder="State"
		bind:value={$form.state}
		bind:error={$form.errors.state} />
	<TextInput
		id="zip_code"
		class="py-2"
		label="Zip Code"
		placeholder="Zip Code"
		bind:value={$form.zipCode}
		bind:error={$form.errors.zipCode} />
	<div class="py-2">
		<label
			for="organization_id"
			class="block font-bold leading-6 text-gray-900">Organization</label>
		<select
			id="organization_id"
			class="mt-2 block w-full rounded-md border-0 px-3 py-2 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
			placeholder="Select an organization"
			bind:value={$form.organizationId}>
			<option value="" disabled>Select an organization</option>
			{#each organizations as organization}
				<option value={organization.id}>{organization.name}</option>
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
