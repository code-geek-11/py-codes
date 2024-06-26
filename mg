import pandas as pd
import glob

def merge_and_sort_csv_files(input_folder, output_csv):
    # Get a list of all CSV files in the input folder
    csv_files = glob.glob(f"{input_folder}/*.csv")
    
    # Read and concatenate all CSV files into a single DataFrame
    df_list = [pd.read_csv(file) for file in csv_files]
    combined_df = pd.concat(df_list, ignore_index=True)
    
    # Sort the DataFrame by the "Size (bytes)" column in descending order
    sorted_df = combined_df.sort_values(by="Size (bytes)", ascending=False)
    
    # Write the sorted DataFrame to the output CSV file
    sorted_df.to_csv(output_csv, index=False)

if __name__ == "__main__":
    input_folder = input("Enter the folder containing the CSV files: ")
    output_csv_file = input("Enter the output CSV file name (with .csv extension): ")
    merge_and_sort_csv_files(input_folder, output_csv_file)
    print(f"All CSV files have been merged and sorted into {output_csv_file}")
