import LoginDialogButton from "./LoginDialogButton";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { getUserInfo } from "~/api/getUserInfo";
import { Avatar, AvatarFallback } from "~/components/ui/avatar";
import { REACT_QUERY_USER_INFO_KEY, TOKEN_KEY } from "~/lib/consts";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "~/components/ui/dropdown-menu";
import { useRouter } from "next/navigation";

const UserInfo = () => {
  const queryClient = useQueryClient();
  const router = useRouter();

  const { data } = useQuery({
    queryKey: [REACT_QUERY_USER_INFO_KEY],
    queryFn: getUserInfo,
  });

  if (!data) {
    return <LoginDialogButton />;
  }

  const handleLogout = () => {
    sessionStorage.removeItem(TOKEN_KEY);
    queryClient.removeQueries({ queryKey: [REACT_QUERY_USER_INFO_KEY] });
    router.refresh();
  };

  return (
    <div className="flex flex-row">
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Avatar className="hover:cursor-pointer">
            <AvatarFallback>
              {data?.firstName[0]?.toUpperCase() ?? ""}
            </AvatarFallback>
          </Avatar>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
          <DropdownMenuLabel>My Account</DropdownMenuLabel>
          <DropdownMenuSeparator />
          <DropdownMenuGroup>
            <DropdownMenuItem>Rents History</DropdownMenuItem>
          </DropdownMenuGroup>
          <DropdownMenuSeparator />
          <DropdownMenuItem onClick={() => handleLogout()}>
            Log out
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
};

export default UserInfo;
