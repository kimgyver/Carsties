import { Button } from "flowbite-react";
import Link from "next/link";
import React from "react";

type Props = {
  id: string;
};

export default function EditButton({ id }: Props) {
  console.log(id);
  return (
    <Button outline>
      <Link href={`/auctions/update/${id}`}>Update Auction</Link>
    </Button>
  );
}
