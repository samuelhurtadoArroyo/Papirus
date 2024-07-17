import { textConstants } from "@/domain/globalization/es";
import Link from "next/link";

export default function Footer({
  showLightBackground,
  showLink,
}: {
  showLightBackground: boolean;
  showLink: boolean;
}) {
  const { information, links } = textConstants.components.layout.footer;

  const className = {
    link: "text-[--link] hover:text-[--link-focus] hover:underline",
    footer: `fixed z-10 inset-x-0 bottom-0 text-center py-5 ${
      showLightBackground
        ? "bg-[--transparent-background-color]"
        : "bg-[--papirus-purple]"
    } 
    ${showLightBackground ? "text-[--text-default]" : "text-[--white]"}`,
  };

  return (
    <footer id="footer" className={className.footer}>
      <small id="footer-text">
        {information}
        {showLink ? (
          <>
            <span> | </span>
            <Link id="privacy-policy-a" href="/privacy-policy" className={className.link}>
              {links.privacyPolicy}
            </Link>
          </>
        ) : null}
      </small>
    </footer>
  );
}
