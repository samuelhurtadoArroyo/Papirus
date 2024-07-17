import { ReactNode } from "react";

export default async function SignInLayout({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <section className="flex flex-col flex-grow items-center justify-center bg-[--papirus-purple] gap-2 p-2 lg:p-0">
      {children}
    </section>
  );
}
