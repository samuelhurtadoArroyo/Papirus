import { textConstants } from "@/domain/globalization/es";
import { ProgressSpinner } from "primereact/progressspinner";
import ScrollToTop from "@/components/common/ScrollToTop";

export default function GlobalLoading() {
  const loadingText = textConstants.components.loading.loadingView;

  return (
    <>
      <ScrollToTop/>
      <main className="flex flex-col flex-grow items-center bg-[--background-color] pt-[30px] pb-20 px-2 lg:px-0">
        <section className="container__flex--center">
          <ProgressSpinner id={loadingText} aria-label={loadingText} />
        </section>
      </main>
    </>
  );
};
