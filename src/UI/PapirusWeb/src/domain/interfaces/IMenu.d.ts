import { MenuItem } from "primereact/menuitem";

declare module "primereact/menuitem" {
  interface MenuItem {
		label: string
		icon?: string
		href?: string
		badge?: number
		items?: MenuItem [] | MenuItem [][] | null
		parent?: string
  };
}