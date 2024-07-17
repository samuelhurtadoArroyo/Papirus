import { getGuardianship } from "@/actions/guardianships/getGuardianshipById";
import { ERoles } from "@/domain/enums/ERoles";
import getSession from "@/services/getSession";
import { notFound } from "next/navigation";
import { ReactNode } from "react";

export default async function GuardianshipLayout({
  children,
  params,
}: {
  children: ReactNode;
  params: { id: string };
}) {
  const session = await getSession();
  const guardianship = await getGuardianship(Number.parseInt(params?.id));

  if (
    !guardianship ||
    (!guardianship?.isCurrentAssigned &&
      session?.user?.roleId !== ERoles.SuperAdmin &&
      session?.user?.roleId !== ERoles.Lead)
  ) {
    notFound();
  }

  return (
    <section className="container__flex--top gap-[30px]">{children}</section>
  );
}
