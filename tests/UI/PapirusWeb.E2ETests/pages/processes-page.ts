import { expect, Locator, Page } from "@playwright/test";

export class ProcessesPage {
  page: Page;
  title: Locator;
  guardianships: Locator;
  demands: Locator;
  loginButton: Locator;
  guardianshipsLink: Locator;
  demandsLink: Locator;
  teamsLink: Locator;
  security: Locator;
  usersLink: Locator;
  administration: Locator;
  processes: Locator;
  userMenu: Locator;
  changePassword: Locator;
  logout: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#processes-h1");
    this.guardianships = page.locator("#guardianships-btn");
    this.demands = page.locator("#demands-btn");
    this.guardianshipsLink = page.locator("#guardianships-a");
    this.demandsLink = page.locator("#demands-a");
    this.teamsLink = page.locator("#teams-a");
    this.security = page.locator("#security-btn");
    this.usersLink = page.locator("#users-a");
    this.administration = page.locator("#administration-btn");
    this.processes = page.locator("#processes-btn");
    this.userMenu = page.locator("#user-menu-btn");
    this.changePassword = page.locator("#update-password-a");
    this.logout = page.locator("#logout-btn");
  }

  async assertProcessesPage() {
    await expect(this.title).toBeVisible();
    await expect(this.guardianships).toBeVisible();
    await expect(this.administration).toBeVisible();
    await expect(this.processes).toBeVisible();
    await expect(this.logout).toBeVisible();
  }

  async clickGuardianshipsBtn() {
    await this.assertProcessesPage();
    await this.guardianships.click();
  }

  async clickDemandsBtn() {
    await this.assertProcessesPage();
    await this.demands.click();
  }

  async clickGuardianshipsLink() {
    await this.assertProcessesPage();
    await this.processes.click();
    await this.guardianshipsLink.click();
  }

  async clickDemandsLink() {
    await this.assertProcessesPage();
    await this.processes.click();
    await this.demandsLink.click();
  }

  async clickTeamsLink() {
    await this.assertProcessesPage();
    await this.administration.click();
    await this.teamsLink.click();
  }

  async clickUsersLink() {
    await this.assertProcessesPage();
    await this.administration.click();
    await this.security.hover();
    await this.usersLink.click();
  }

  async clickChangePassword() {
    await this.assertProcessesPage();
    await this.userMenu.click();
    await this.changePassword.click();
  }

  async clickLogout() {
    await this.assertProcessesPage();
    await this.logout.click();
  }
}
