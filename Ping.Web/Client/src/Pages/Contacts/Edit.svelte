<script>
	import { page } from '@inertiajs/svelte'
	import ContactForm from '../../Shared/ContactForm.svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'

	$: ({ contact, organizations } = $page.props)
</script>

<PageLayout title="Edit Contact">
	<ContactForm method="edit" model={contact} {organizations} />
</PageLayout>
