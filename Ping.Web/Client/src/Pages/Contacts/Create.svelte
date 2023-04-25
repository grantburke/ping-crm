<script>
	import { page } from '@inertiajs/svelte'
	import ContactForm from '../../Shared/ContactForm.svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'

	$: ({ organizations } = $page.props)
</script>

<PageLayout title="Create Contact">
	<ContactForm {organizations} />
</PageLayout>
