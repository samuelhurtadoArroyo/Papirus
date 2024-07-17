import { Footer, Header } from "@/components/layout";
import { textConstants } from "@/domain/globalization/es";
import { Metadata } from "next";
import { SessionProvider } from "next-auth/react";
import getSession from "@/services/getSession";
import PermissionsProvider from "@/providers/permissions";
import { getPermissions } from "@/actions/permissions/getPermissions";

const pageinfo = textConstants.pages.processes;

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default async function PrivateLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const session = await getSession();
  const permissions = await getPermissions();

  return (
    <SessionProvider session={session}>
      <PermissionsProvider permissions={permissions}>
        <Header />
        <main className="flex flex-col flex-grow items-center bg-[--background-color] pt-[30px] pb-20 px-2 lg:px-0">
          {children}
        </main>
        <Footer showLightBackground showLink />
      </PermissionsProvider>
    </SessionProvider>
  );
}
