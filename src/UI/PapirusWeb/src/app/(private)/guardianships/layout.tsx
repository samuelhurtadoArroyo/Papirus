import { textConstants } from "@/domain/globalization/es";
import { Metadata } from "next";
import { ReactNode } from "react";

const pageinfo = textConstants.pages.guardianships;

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default function GuardianshipsLayout({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <section className="container__flex--top gap-[30px]">{children}</section>
  );
}
