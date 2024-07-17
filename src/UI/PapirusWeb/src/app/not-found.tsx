import { textConstants } from "@/domain/globalization/es";

export default function GlobalNotFound() {
  const notFoundText = textConstants.pages.notFound.title;
  return (
    <main className="flex flex-col flex-grow items-center bg-[--background-color] pt-[30px] pb-20 px-2 lg:px-0">
      <section className="container__flex--center">
        <p id="not-found-p" className="flex">{notFoundText}</p>
      </section>
    </main>
  );
}
