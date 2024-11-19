"use client";

import { usePathname } from "next/navigation";
import Link from "next/link";
import UserInfo from "./UserInfo";

const PATH_WITH_HIDDEN_NAV = ["/complete-registration"];

const Navbar = () => {
  const pathname = usePathname();

  if (PATH_WITH_HIDDEN_NAV.includes(pathname)) {
    return null;
  }

  return (
    <nav className="w-full border-b border-gray-800">
      <div className="mx-auto px-6 lg:px-10">
        <div className="flex h-16 items-center justify-between">
          <div className="flex-shrink-0">
            <Link href="/">
              <span className="text-xl font-bold">Car Rent Browser</span>
            </Link>
          </div>
          <div className="flex items-center">
            <UserInfo />
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
