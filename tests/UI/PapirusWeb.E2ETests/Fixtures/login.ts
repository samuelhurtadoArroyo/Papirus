import { test, expect } from "@playwright/test";
import { LoginPage } from "../pages/login-page";
import dotenv from "dotenv";

dotenv.config();

const extendedTest = test.extend({
  webApp: async ({ page }, use) => {
    await page.goto(process.env.PAPIRUS_HOST || "/");
    const loginPage = new LoginPage(page);
    await loginPage.submitLoginForm(
      process.env.PAPIRUS_USER,
      process.env.PAPIRUS_PASSWORD
    );
    await use(page);
  },
});

export { expect, extendedTest as test };
