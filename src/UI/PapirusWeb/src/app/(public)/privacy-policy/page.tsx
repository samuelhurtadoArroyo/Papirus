import { textConstants } from "@/domain/globalization/es";
import React from "react";

const page = () => {
  const availableSoonText = textConstants.pages.availableSoon.title;
  return (
    <section className="container__flex--center w-full max-w-none">
      <p>{availableSoonText}</p>
    </section>
  );
};

export default page;
