"use client";
import { textConstants } from "@/domain/globalization/es";
import { IUser } from "@/domain/interfaces/IUser";
import { getUserMenuOptions } from "@/services/getUserMenuOptions";
import { Montserrat } from "next/font/google";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { Menu } from "primereact/menu";
import React, { useRef } from "react";
const montserratFont = Montserrat({ subsets: ["latin"] });

const UserDropdownMenu = ({ user }: { user: IUser }) => {
  const menu = useRef<Menu>(null);
  const altIconText = textConstants.components.alt.icons;
  const router = useRouter();

  return (
    <React.Fragment>
      <Menu
        model={getUserMenuOptions({ redirect: router.push })}
        popup
        ref={menu}
        id="popup_menu_left"
        className="mt-3 p-0 w-max"
        closeOnEscape
        pt={{
          root: { className: montserratFont.className },
          menuitem: { className: "font-medium" },
        }}
      />
      <button
        id="user-menu-btn"
        className="items-center justify-center gap-3 flex font-medium hover:backdrop-brightness-95 disabled:backdrop-filter-none md:p-4 p-1"
        type="button"
        onClick={(event) => menu?.current?.toggle(event)}
        aria-controls="popup_menu_left"
        aria-haspopup
        disabled // NOTE: disabled until user update endpoint is available
      >
        <Image
          alt={altIconText.user}
          src="/user.svg"
          width={14}
          height={14}
          className="aspect-auto"
        />
        <span className="hidden md:flex font-medium text-base">
          {user?.name}
        </span>
      </button>
    </React.Fragment>
  );
};

export default UserDropdownMenu;
