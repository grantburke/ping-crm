<script>
	import { useForm } from '@inertiajs/svelte'
	import TextInput from '../Shared/TextInput.svelte'

	export let method = 'create'
	export let model = {
		id: 0,
		name: '',
		email: '',
		phone: '',
		address: '',
		city: '',
		state: '',
		zipCode: '',
	}

	console.log(model)

	let form = useForm(model)

	function handle_submit() {
		$form.clearErrors()
		if (method !== 'create') 
         $form.put(`/organizations/${model.id}/edit`)
      else
         $form.post(`/organizations/create`)
	}
</script>

<form
	class="mx-auto w-full rounded bg-white px-8 py-4 shadow"
	on:submit|preventDefault={handle_submit}>
	<TextInput
		class="py-2"
		id="name"
		label="Name"
		placeholder="Name"
		bind:value={$form.name}
		bind:error={$form.errors.name} />
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
		<button
			type="submit"
			class="rounded-md bg-gray-800 px-4 py-2 text-white hover:bg-indigo-600">
			Submit
      </button>
	</div>
</form>
