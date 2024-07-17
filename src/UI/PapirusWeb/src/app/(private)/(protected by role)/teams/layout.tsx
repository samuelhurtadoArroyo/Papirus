import { textConstants } from "@/domain/globalization/es";
import { Metadata } from "next";
import { ReactNode } from "react";

const pageinfo = textConstants.pages.teams;

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default function TeamsLayout({ children }: { children: ReactNode }) {
  return <section className="container__flex--top gap-[30px] px-1">{children}</section>;
}
