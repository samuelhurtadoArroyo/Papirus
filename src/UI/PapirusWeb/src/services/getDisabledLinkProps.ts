import { AnchorHTMLAttributes } from "react";

export const getDisabledLinkProps = (disabled: boolean) => {
	return {
		"aria-disabled": disabled,
		style: {
			pointerEvents: disabled ? "none" : "auto",
		},
		tabIndex: disabled ? -1 : undefined,
	} satisfies AnchorHTMLAttributes<HTMLAnchorElement>;
};
