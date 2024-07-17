"use client";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "next-auth/react";
import Image from "next/image";
export default function Logout() {
  const altIconText = textConstants.components.alt.icons;
  const logoutText = textConstants.components.auth.logout.button;

  const onLogout = () => {
    signOut();
  };

  return (
    <button
      id="logout-btn"
      onClick={onLogout}
      className="flex items-center gap-3 hover:bg-[--no-bg-hover] rounded-[4px] text-base font-medium hover:backdrop-brightness-95 md:p-4 p-1"
    >
      <Image
        alt={altIconText.logout}
        src="/logout.svg"
        width={16}
        height={14}
        className="aspect-auto"
      />
      <span className=" hidden md:flex">{logoutText}</span>
    </button>
  );
}
