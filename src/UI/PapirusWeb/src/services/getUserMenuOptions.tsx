import { textConstants } from "@/domain/globalization/es";
import { MenuItem } from "primereact/menuitem";

export const getUserMenuOptions = ({
  redirect,
}: {
  redirect: (url: string) => void;
}) => {
  const menuOption = textConstants.components.layout.navBar.userLinks;

  const menu: MenuItem[] = [
    {
      id: "update-password-a",
      label: menuOption.updatePassword,
      command: () => redirect("/settings/update-password"),
    },
  ];

  return menu;
};
