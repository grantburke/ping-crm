<script>
	import { page } from '@inertiajs/svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'
	import UserForm from '../../Shared/UserForm.svelte'

	$: ({ roles } = $page.props)
</script>

<PageLayout title="Create User">
	<UserForm {roles} />
</PageLayout>
