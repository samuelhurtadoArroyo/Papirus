import "primereact/resources/primereact.min.css";
import "primereact/resources/themes/md-light-deeppurple/theme.css";
import "primeicons/primeicons.css";
import "@/styles/globals.css";
import { Montserrat } from "next/font/google";
import { PrimeReactProvider } from "primereact/api";
import type { Metadata } from "next";
import Head from "next/head";

const montserratFont = Montserrat({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Papirus",
  description: "Papirus",
  icons: { icon: "/papirus-favicon.png", apple: "/papirus-favicon.png" },
};

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="es">
      <Head>
        <script
          dangerouslySetInnerHTML={{
            __html: `
              const style = document.createElement('style')
              style.innerHTML = '@layer tailwind-base, primereact, tailwind-utilities;'
              style.setAttribute('type', 'text/css')
              document.querySelector('head').prepend(style)
            `,
          }}
        />
      </Head>
      <body className={montserratFont.className}>
        <PrimeReactProvider>{children}</PrimeReactProvider>
      </body>
    </html>
  );
}
