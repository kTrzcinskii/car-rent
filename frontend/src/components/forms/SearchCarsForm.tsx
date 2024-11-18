"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import { Input } from "../ui/input";
import { Button } from "../ui/button";
import { Search } from "lucide-react";
import { type Dispatch, type SetStateAction } from "react";
import { useSearchParams, useRouter } from "next/navigation";

const formSchema = z.object({
  ModelName: z.string().optional(),
  BrandName: z.string().optional(),
});

type FormSchemaType = z.infer<typeof formSchema>;

interface ISearchCarsFromProps {
  defaultModelName: string;
  setModelName: Dispatch<SetStateAction<string>>;
  defaultBrandName: string;
  setBrandName: Dispatch<SetStateAction<string>>;
}

const SearchCarsForm = (props: ISearchCarsFromProps) => {
  const searchParams = useSearchParams();
  const router = useRouter();

  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      BrandName: props.defaultBrandName,
      ModelName: props.defaultModelName,
    },
  });

  const onSubmit = (values: FormSchemaType) => {
    const params = new URLSearchParams(searchParams.toString());
    props.setBrandName(values.BrandName ?? "");
    params.set("brandName", values.BrandName ?? "");
    props.setModelName(values.ModelName ?? "");
    params.set("modelName", values.ModelName ?? "");
    router.replace(`/browse?${params.toString()}`);
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="relative mx-auto flex flex-col items-center justify-center space-y-8 lg:flex-row lg:items-end lg:space-x-5 lg:space-y-0"
      >
        <FormField
          control={form.control}
          name="BrandName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Brand name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="ModelName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Model name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">
          <Search className="mr-2 h-4 w-4" />
          Search
        </Button>
      </form>
    </Form>
  );
};

export default SearchCarsForm;
