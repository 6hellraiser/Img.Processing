#include <iostream>
#include <opencv2\highgui\highgui.hpp>
#include <opencv2\core\core.hpp>
#include <cv.h>
using namespace std;
using namespace cv;

int main() {
	//display the original image
        //IplImage* img = cvLoadImage("C:\\cat.jpg");
        //cvNamedWindow("MyWindow");
        //cvShowImage("MyWindow", img);

        ////invert and display the inverted image
        //cvNot(img, img);
        //cvNamedWindow("Inverted");
        //cvShowImage("Inverted", img);

        //cvWaitKey(0);
       
        ////cleaning up
        //cvDestroyWindow("MyWindow");
        //cvDestroyWindow("Inverted");
        //cvReleaseImage(&img);
	 // OPENCV CHEAT SHEET
	Mat M(5,4, CV_16UC3, Scalar(1,2,3));
	cout << "M=" << endl << ' '<< M << endl;
	cout << M.rows << M.cols << ' ' << M.depth() << M.channels() << endl;
	//redefinition of the matrix; memory is initialized but not yet used
	M.create(4,4,CV_16UC3);
	cout << "M=" << endl << ' '<< M << endl;
	//fill the matrix
	randu(M, 0, 10);
	cout << "M=" << endl << ' '<< M << endl;
	//sharing the same data
	Mat M1 = M;
	Mat M2; M2 = M;
	M1=0; //filling with zeros: all three matrices are zeros now
	M1.create(3,4,CV_16UC3); //this matrix doesn't share that data any more; link counter--

	M = Mat::ones(4,4,CV_16UC3); //3-channel matrix; only the first channel has ones;
	M = Mat::eye(4,4,CV_16UC3);
	cout << "M=" << endl << ' '<< M << endl;

	double a = CV_PI/3;
	Mat A22 = (Mat_<float>(2,2) << cos(a), -sin(a), sin(a), cos(a)); // rotation matrix (поворота)
	cout << "A22=" << endl << ' '<< A22 << endl;
	cout << A22.at<float>(1,0) << endl;
	float sum = 0;
	for (int i = 0; i < A22.rows; i++)
	{
		float* p = A22.ptr<float>(i); 
		for (int j = 0; j < A22.cols; j++)
		{
			sum += *p++;
		}
	}
	cout << sum << endl;
	cout << M.at<Vec3w>(1,0)[2] << endl;
	cin.ignore();
	return 0;

}