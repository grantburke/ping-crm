<script>
	import { page } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import OrganizationForm from '../../Shared/OrganizationForm.svelte'

	$: ({ organization } = $page.props)
</script>

<PageLayout title="Edit Organization">
	<OrganizationForm method="edit" model={organization} />
</PageLayout>
