from flask import Flask, request, jsonify
from pyspark.sql import SparkSession
import os

app = Flask(__name__)
app.config['UPLOAD_FOLDER'] = 'uploads'
app.config['MAX_CONTENT_LENGTH'] = 16 * 1024 * 1024  # 16 MB upload limit

# Ensure the upload folder exists
if not os.path.exists(app.config['UPLOAD_FOLDER']):
    os.makedirs(app.config['UPLOAD_FOLDER'])

# Initialize PySpark
spark = SparkSession.builder \
    .appName("FileUploadApp") \
    .getOrCreate()

@app.route('/upload_file', methods=['POST'])
def upload_file():
    if 'file' not in request.files:
        return jsonify(success=False, error="No file part in the request")

    file = request.files['file']
    if file.filename == '':
        return jsonify(success=False, error="No file selected for uploading")

    if file:
        file_path = os.path.join(app.config['UPLOAD_FOLDER'], file.filename)
        file.save(file_path)

        try:
            # Load the file into a PySpark DataFrame
            df = spark.read.format("csv").option("header", "true").load(file_path)
            df.show()  # Show the DataFrame content for debugging purposes

            return jsonify(success=True, message="File uploaded and processed successfully!")
        except Exception as e:
            return jsonify(success=False, error=str(e))
    else:
        return jsonify(success=False, error="Unknown error occurred")

if __name__ == '__main__':
    app.run(debug=True)
