import { textConstants } from "@/domain/globalization/es";
import { Button } from "../common";
import { useFormStatus } from "react-dom";
import { useRouter } from "next/navigation";
import { usePermissions } from "@/hooks/usePermissions";

const UserSubmit = () => {
  const { pending } = useFormStatus();
  const router = useRouter();
  const { validatePermission, permissionConstants } = usePermissions();
  const usersFormText = textConstants.components.usersForm.form;

  return (
    <div className="flex w-full justify-between">
      <Button
        id="cancel-btn"
        label={usersFormText.cancel}
        type="button"
        onClick={() => router.push("/users")}
        variant={"secondary"}
        className="px-[42px] uppercase h-10"
        disabled={pending}
      />
      <Button
        id="save-btn"
        label={usersFormText.save}
        type="submit"
        variant={"primary"}
        className="px-[42px] uppercase h-10"
        disabled={
          pending ||
          !validatePermission(permissionConstants.users.create) ||
          !validatePermission(permissionConstants.users.edit)
        }
      />
    </div>
  );
};

export default UserSubmit;
