import { textConstants } from '@/domain/globalization/es';
import { Metadata } from 'next';
import { ReactNode } from 'react';

const pageinfo = textConstants.pages.createUser

export const metadata: Metadata = {
  title: pageinfo.pageTitle,
  description: pageinfo.pageDescription,
};

export default function UsersCreateLayout({ children }: { children: ReactNode }) {
  return children
}
