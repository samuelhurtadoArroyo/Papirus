import { Footer } from "@/components/layout";

export default async function PublicLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <>
      <main className="flex flex-col flex-grow">{children}</main>
      <Footer showLightBackground={false} showLink={false} />
    </>
  );
}
