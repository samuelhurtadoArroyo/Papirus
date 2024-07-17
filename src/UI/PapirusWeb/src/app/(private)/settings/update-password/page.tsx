import { SubHeader } from "@/components/layout";
import UpdatePasswordForm from "@/components/user-settings/UpdatePasswordForm";
import { textConstants } from "@/domain/globalization/es";

const UpdatePassword = () => {
  const updatePasswordText = textConstants.pages.updatePassword.header;

  return (
    <>
      <SubHeader title={updatePasswordText.title}/>
      <UpdatePasswordForm/>
    </>
  );
}


export default UpdatePassword