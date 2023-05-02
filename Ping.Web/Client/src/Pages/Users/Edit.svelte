<script>
	import { page } from '@inertiajs/svelte'
	import UserForm from '../../Shared/UserForm.svelte'
	import PageLayout from '../../Shared/PageLayout.svelte'

	$: ({ user, roles } = $page.props)
</script>

<PageLayout title="Edit User">
	<UserForm method="edit" model={user} {roles} />
</PageLayout>
