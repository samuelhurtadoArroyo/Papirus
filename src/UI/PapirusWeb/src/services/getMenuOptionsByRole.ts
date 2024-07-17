import { textConstants } from "@/domain/globalization/es";
import { MenuItem } from "primereact/menuitem";
import { getProcesses } from "./getProcesses";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { EProcesses } from "@/domain/enums/EProcesses";

export const getMenuOptionsByRole = ({
  redirect,
  permissionConstants,
  validatePermission,
}: {
  redirect: (url: string) => void;
  permissionConstants: typeof PERMISSIONS;
  validatePermission: (permission: string) => boolean;
}) => {
  const menuOption = textConstants.components.layout.navBar.links;

  const menu: MenuItem[] = [
    {
      id: "processes-btn",
      label: menuOption.processes,
      items: getProcesses().map(
        (process) =>
          ({
            id: `${process.id}-a`,
            label: process.name,
            command: () => (process?.url ? redirect(process?.url) : null),
            disabled:
              (process.id === EProcesses.Demands &&
                !validatePermission(permissionConstants.demands.view)) ||
              (process.id === EProcesses.Guardianships &&
                !validatePermission(permissionConstants.guardianships.view)),
          } as MenuItem)
      ),
    },
  ];

  if (
    validatePermission(permissionConstants.config.view) ||
    validatePermission(permissionConstants.teams.view) ||
    validatePermission(permissionConstants.users.view)
  ) {
    menu.push({
      id: "administration-btn",
      label: menuOption.administration,
      items: [
        {
          id: "config-a",
          label: menuOption.config,
          command: () => redirect("/config"),
          disabled: !validatePermission(permissionConstants.config.view),
        },
        {
          id: "teams-a",
          label: menuOption.teams,
          command: () => redirect("/teams"),
          disabled: !validatePermission(permissionConstants.teams.view),
        },
        {
          id: "security-btn",
          label: menuOption.security,
          disabled: !validatePermission(permissionConstants.users.view),
          items: [
            {
              id: "users-a",
              label: menuOption.users,
              command: () => redirect("/users"),
              disabled: !validatePermission(permissionConstants.users.view),
            },
          ],
        },
      ] as MenuItem[],
    });
  }
  return menu;
};
