import { ReactNode } from "react";

export default function SubHeader({
  children,
  title,
}: {
  children?: ReactNode;
  title: string;
}) {
  return (
    <section className="flex items-center justify-between bg-[--white] px-5 md:px-[30px] h-[90px] w-full rounded-lg shadow-md gap-x-5 gap-y-2 flex-wrap">
      <h1 id="title-h1" className="text-xl font-semibold uppercase">{title}</h1>
      <div className="flex gap-5">{children}</div>
    </section>
  );
}
