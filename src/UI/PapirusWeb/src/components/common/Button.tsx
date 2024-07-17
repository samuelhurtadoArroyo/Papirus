"use client";
import { ButtonProps, Button as PrimeButton } from "primereact/button";
import { ReactNode } from "react";
import "primereact/resources/themes/md-light-deeppurple/theme.css";
import { Montserrat } from "next/font/google";
const montserratFont = Montserrat({ subsets: ["latin"] });

export default function Button({
  id,
  children,
  isPending,
  className,
  variant,
  ...props
}: {
  id: string;
  children?: ReactNode;
  isPending?: boolean;
  variant: "primary" | "secondary" | "danger";
} & ButtonProps) {
  const classNameByVariant = {
    primary:
      "bg-[--button-medium] text-[--white] border-2 border-solid border-[--button-medium] disabled:bg-[--button-bg-disabled] disabled:text-[--button-text-disabled] disabled: border-none",
    secondary:
      "bg-[--white] text-[--button-medium] border-2 border-solid border-[--button-medium] disabled:bg-[--white] disabled:text-[--button-text-disabled] disabled:border-[--button-text-disabled]",
    danger:
      "bg-[--error] text-[--white] border-2 border-solid border-[--button-medium] disabled:bg-[--button-bg-disabled] disabled:text-[--button-text-disabled] disabled: border-none",
  };

  return (
    <PrimeButton
      id={id}
      className={`flex justify-center items-center px-[1rem] text-sm font-medium rounded-full ${
        isPending ? "bg-[--button-disabled]" : classNameByVariant[variant]
      } ${
        isPending ? "text-[--text-placeholder]" : classNameByVariant[variant]
      }  hover:shadow-md disabled:hover:shadow-none ${className}`}
      {...props}
      pt={{
        root: {
          className:
            montserratFont.className +
            " focus-visible:outline-offset-1 focus-visible:outline-1 focus-visible:outline-black focus-visible:border-2 focus-visible:border-black",
        },
      }}>
      {children}
    </PrimeButton>
  );
}
