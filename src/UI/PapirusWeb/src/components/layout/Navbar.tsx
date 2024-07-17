"use client";
import Logout from "@/components/auth/Logout";
import { textConstants } from "@/domain/globalization/es";
import { getMenuOptionsByRole } from "@/services/getMenuOptionsByRole";
import Image from "next/image";
import Link from "next/link";
import { Menubar } from "primereact/menubar";
import { Montserrat } from "next/font/google";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import UserDropdownMenu from "../user-settings/UserDropdownMenu";
const montserratFont = Montserrat({ subsets: ["latin"] });
import { useRouter } from "next/navigation";
import { usePermissions } from "@/hooks/usePermissions";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";

export default function Navbar() {
  const altLogoText = textConstants.components.alt.logo;
  const altIconText = textConstants.components.alt.icons;
  const { validatePermission, permissionConstants } = usePermissions();
  const user = useCurrentUser();
  const router = useRouter();

  const start = (
    <Link
      id="home-a"
      href={"/"}
      {...getDisabledLinkProps(
        !validatePermission(permissionConstants.processes.view)
      )}>
      <Image
        alt={altLogoText}
        src="/logo.svg"
        width={130}
        height={33}
        className="aspect-auto"
      />
    </Link>
  );
  const end = (
    <section className="flex items-center justify-between gap-6 h-8 text-[--white]">
      {user ? <UserDropdownMenu user={user} /> : null}
      <Image
        alt={altIconText.divider}
        src="/divider.svg"
        width={2}
        height={32}
        className="h-full"
      />
      <Logout />
    </section>
  );
  return (
    <Menubar
      id="layout-menu"
      model={getMenuOptionsByRole({
        redirect: router.push,
        validatePermission,
        permissionConstants,
      })}
      start={start}
      end={end}
      className={
        "bg-[--papirus-purple] h-20 flex justify-between items-center rounded-none w-full border-0 p-0 gap-10 px-2 lg:px-0"
      }
      pt={{
        root: {
          className: montserratFont.className,
        },
        button: {
          className: "order-first lg:hidden",
        },
        menu: { className: "lg:flex lg:gap-2" },
        submenuIcon: { hidden: true },
        menuitem: { className: "font-medium" },
      }}
    />
  );
}
