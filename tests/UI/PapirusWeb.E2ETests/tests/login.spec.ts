import { test, expect } from "@playwright/test";
import { LoginPage } from "../pages/login-page";
import dotenv from "dotenv";

dotenv.config();

test("1 - Succesful Login", async ({ page }) => {
  await page.goto(process.env.PAPIRUS_HOST || "/");
  const loginPage = new LoginPage(page);
  await loginPage.submitLoginForm(
    process.env.PAPIRUS_USER,
    process.env.PAPIRUS_PASSWORD,
  );
  await expect(page.locator("#processes-h1")).toBeVisible();
});

test("2 - Failed Login", async ({ page }) => {
  await page.goto(process.env.PAPIRUS_HOST || "/");
  const loginPage = new LoginPage(page);
  await loginPage.submitLoginForm("error_user@email.com", "error_password");
  await expect(page.locator(".p-toast-message-error")).toBeVisible();
});
