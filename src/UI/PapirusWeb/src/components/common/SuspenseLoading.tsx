import React from 'react'
import ScrollToTop from './ScrollToTop';
import { ProgressSpinner } from 'primereact/progressspinner';
import { textConstants } from '@/domain/globalization/es';

const SuspenseLoading = () => {
	const loadingText = textConstants.components.loading.loadingView;
	return (
    <>
      <ScrollToTop />
      <main className="flex flex-col flex-grow items-center pt-[30px] pb-20 px-2 lg:px-0">
        <section className="container__flex--center">
          <ProgressSpinner id={loadingText} aria-label={loadingText} />
        </section>
      </main>
    </>
  );
}

export default SuspenseLoading