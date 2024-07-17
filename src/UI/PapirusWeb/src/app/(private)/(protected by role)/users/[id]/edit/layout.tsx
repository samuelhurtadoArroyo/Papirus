import { textConstants } from '@/domain/globalization/es';
import { Metadata } from 'next';
import { ReactNode } from 'react';

const pageinfo = textConstants.pages.editUser

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default function UsersEditLayout({ children }: { children: ReactNode }) {
  return children
}
