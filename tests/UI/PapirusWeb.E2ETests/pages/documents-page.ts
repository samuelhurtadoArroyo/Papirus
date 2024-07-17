import { expect, Locator, Page } from "@playwright/test";

export class DocumentsPage {
  page: Page;
  title: Locator;
  return: Locator;
  redirectToDocument: Locator;
  continue: Locator;
  download: Locator;
  documentIframe: Locator;

  constructor(page: Page) {
    this.page = page;
    this.title = page.locator("#title-h1");
    this.return = page.locator("#return-btn");
    this.redirectToDocument = page.locator("#view-document-1-a");
    this.continue = page.locator("#continue-btn");
    this.download = page.locator("#download-btn");
    this.documentIframe = page.locator("#document-iframe");
  }

  async assertDocumentsPage() {
    await expect(this.title).toBeVisible();
    await expect(this.return).toBeVisible();
  }

  async assertDocumentPage() {
    await expect(this.title).toBeVisible();
    await expect(this.return).toBeVisible();
    await expect(this.documentIframe).toBeVisible();
  }

  async clickRedirectToDocument() {
    await this.redirectToDocument.click();
  }

  async clickContinue() {
    await this.continue.click();
  }

  async clickDownload() {
    await this.download.click();
  }

  async assertErrorToast() {
    await expect(this.page.locator(".p-toast-message-error")).toBeVisible();
  }
}
