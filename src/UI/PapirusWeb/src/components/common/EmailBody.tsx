import { Montserrat } from "next/font/google";

const defaultFont = Montserrat({ subsets: ["latin"] });

const EmailBody = ({ emailHtmlBody }: { emailHtmlBody: string }) => {
  return (
    <div
      className={`${defaultFont.className}  p-4 overflow-auto h-full`}
      dangerouslySetInnerHTML={{
        __html: emailHtmlBody ?? "<span>No data</span>",
      }}></div>
  );
};

export default EmailBody;
