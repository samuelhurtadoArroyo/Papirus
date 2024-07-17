import { textConstants } from "@/domain/globalization/es";
import { Metadata } from "next";

const pageinfo = textConstants.pages.notFound;

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default async function NotFoundLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return children;
}
