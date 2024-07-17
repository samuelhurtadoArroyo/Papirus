import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { Metadata } from "next";
import { SessionProvider } from "next-auth/react";
import { ReactNode } from "react";

const pageinfo = textConstants.pages.updatePassword;

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default async function MustChangePasswordLayout({
  children,
}: {
  children: ReactNode;
}) {
  const session = await getSession();

  return (
    <SessionProvider session={session}>
      <section className="flex flex-col flex-grow items-center justify-center bg-[--papirus-purple] gap-2 p-2 lg:p-0">
        {children}
      </section>
    </SessionProvider>
  );
}
